using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Payments.Enums;
using ArtmaisBackend.Core.Payments.Interface;
using ArtmaisBackend.Core.Payments.Request;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using MercadoPago.Client;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Payments.Service
{
    public class PaymentService : IPaymentService
    {
        public PaymentService(IPaymentHistoryRepository paymentHistoryRepository,
                              IPaymentProductRepository paymentProductRepository,
                              IPaymentRepository paymentRepository,
                              IPaymentStatusRepository paymentStatusRepository,
                              IPaymentTypeRepository paymentTypeRepository,
                              IProductRepository productRepository,
                              ISignatureRepository signatureRepository)
        {
            _paymentHistoryRepository = paymentHistoryRepository;
            _paymentProductRepository = paymentProductRepository;
            _paymentRepository = paymentRepository;
            _paymentStatusRepository = paymentStatusRepository;
            _paymentTypeRepository = paymentTypeRepository;
            _productRepository = productRepository;
            _signatureRepository = signatureRepository;
        }

        private readonly IPaymentHistoryRepository _paymentHistoryRepository;
        private readonly IPaymentProductRepository _paymentProductRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISignatureRepository _signatureRepository;
        private readonly string tokenMercadoPago = "TEST-4734890284706792-101621-772162e631cd84775da9d50f2e0acb43-278907011";

        public async Task<Payment> PaymentCreateRequest(PaymentRequest paymentRequest, long userId)
        {
            var request = new PaymentCreateRequest
            {
                TransactionAmount = paymentRequest.TransactionAmount,
                Token = paymentRequest.CardToken,
                Description = paymentRequest.Description,
                Installments = paymentRequest.Installments,
                PaymentMethodId = paymentRequest.PaymentMethodId,
                Payer = new PaymentPayerRequest
                {
                    Email = paymentRequest.Email,
                }
            };

            var requestOptions = new RequestOptions
            {
                AccessToken = tokenMercadoPago
            };

            var insertPayment = await InsertPayment(userId, PaymentTypeEnum.CREDIT).ConfigureAwait(false);
            if (!insertPayment)
            {
                throw new ArgumentNullException();
            }

            var paymentInfo = await GetPaymentByUserId(userId).ConfigureAwait(false);
            if (paymentInfo is null)
            {
                throw new ArgumentNullException();
            }

            var insertCreatedHistoryPayment = await InsertPaymentHistory(paymentInfo.PaymentID, PaymentStatusEnum.CREATED).ConfigureAwait(false);
            if (!insertCreatedHistoryPayment)
            {
                throw new ArgumentNullException();
            }

            var client = new PaymentClient();
            Payment payment = await client.CreateAsync(request, requestOptions);

            if (payment is null)
            {
                throw new ArgumentNullException();
            }

            var product = await GetSignature().ConfigureAwait(false);
            if (product is null)
            {
                throw new ArgumentNullException();
            }

            var paymentProduct = await InsertPaymentProduct(product.ProductID, paymentInfo.PaymentID).ConfigureAwait(false);
            if (!paymentProduct)
            {
                throw new ArgumentNullException();
            }

            await _signatureRepository.Create(userId);

            var updatePayment = await UpdatePayment(paymentInfo).ConfigureAwait(false);
            if (!updatePayment)
            {
                throw new ArgumentNullException();
            }

            var insertDoneHistoryPayment = await InsertPaymentHistory(paymentInfo.PaymentID, PaymentStatusEnum.DONE).ConfigureAwait(false);
            if (!insertDoneHistoryPayment)
            {
                throw new ArgumentNullException();
            }

            return payment;
        }

        private async Task<bool> InsertPayment(long userId, PaymentTypeEnum paymentTypeEnum)
        {
            var payment = await _paymentRepository.Create(userId, (int)paymentTypeEnum);

            if (payment is null)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> UpdatePayment(Entities.Payments paymentRequest)
        {
            paymentRequest.LastUpdateDate = DateTime.UtcNow;
            var payment = await _paymentRepository.Update(paymentRequest);

            if (payment is null)
            {
                return false;
            }

            return true;
        }

        private async Task<Entities.Payments?> GetPaymentByUserId(long userId)
        {
            return await _paymentRepository.GetPaymentByUserId(userId);
        }

        private async Task<Entities.Payments?> GetPaymentByIdAndUserId(int paymentId, long userId)
        {
            return await _paymentRepository.GetPaymentByIdAndUserId(paymentId, userId);
        }

        private async Task<bool> InsertPaymentHistory(int paymentId, PaymentStatusEnum paymentStatusEnum)
        {
            var paymentHistory = await _paymentHistoryRepository.Create(paymentId, (int)paymentStatusEnum);

            if (paymentHistory is null)
            {
                return false;
            }

            return true;
        }

        private async Task<PaymentHistory?> GetPaymentHistoryByPaymentId(int paymentId)
        {
            return await _paymentHistoryRepository.GetPaymentHistoryByPaymentId(paymentId);
        }

        private async Task<bool> InsertPaymentProduct(int productId, int paymentId)
        {
            var paymentProduct = await _paymentProductRepository.Create(productId, paymentId);

            if (paymentProduct is null)
            {
                return false;
            }

            return true;
        }

        private async Task<PaymentProduct?> GetPaymentProductByPaymentId(int paymentId)
        {
            return await _paymentProductRepository.GetPaymentProductByPaymentId(paymentId);
        }

        private async Task<Product?> GetSignature()
        {
            return await _productRepository.GetSignature();
        }

        private async Task<List<Product>> GetProductsByUserId(long userId)
        {
            return await _productRepository.GetProductsByUserId(userId);
        }

        private async Task<PaymentsStatus?> GetPaymentStatus(PaymentStatusEnum paymentStatusEnum)
        {
            return await _paymentStatusRepository.GetPaymentStatusById((int)paymentStatusEnum);
        }

        private async Task<PaymentType?> GetPaymentType(PaymentTypeEnum paymentTypeEnum)
        {
            return await _paymentTypeRepository.GetPaymentTypeById((int)paymentTypeEnum);
        }
    }
}
using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Payments.Enums;
using ArtmaisBackend.Core.Payments.Interface;
using ArtmaisBackend.Core.Payments.Request;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using MercadoPago.Client;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
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
                              IUserRepository userRepository,
                              IMapper mapper)
        {
            _paymentHistoryRepository = paymentHistoryRepository;
            _paymentProductRepository = paymentProductRepository;
            _paymentRepository = paymentRepository;
            _paymentStatusRepository = paymentStatusRepository;
            _paymentTypeRepository = paymentTypeRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        private readonly IPaymentHistoryRepository _paymentHistoryRepository;
        private readonly IPaymentProductRepository _paymentProductRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public async Task<Payment> PaymentCreateRequest(PaymentRequest paymentRequest, long userId)
        {
            MercadoPagoConfig.AccessToken = "YOUR_ACCESS_TOKEN";

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
            var requestOptions = new RequestOptions();
            requestOptions.AccessToken = "YOUR_ACCESS_TOKEN";
            // ...

            var client = new PaymentClient();
            Payment payment = await client.CreateAsync(request, requestOptions);

            if(payment is null)
                throw new ArgumentNullException();

            return payment;
        }


        public async Task<bool> InsertPayment(long userId, PaymentStatusEnum paymentStatusEnum)
        {
            var payment = await _paymentRepository.Create(userId, (int)paymentStatusEnum);

            if (payment is null)
                return false;

            return true;
        }

        public async Task<bool> UpdatePayment(Entities.Payments paymentRequest)
        {
            paymentRequest.LastUpdateDate = DateTime.UtcNow;
            var payment = await _paymentRepository.Update(paymentRequest);

            if (payment is null)
                return false;

            return true;
        }

        public async Task<Entities.Payments?> GetPaymentByUserId(long userId)
        {
            return await _paymentRepository.GetPaymentByUserId(userId);
        }

        public async Task<Entities.Payments?> GetPaymentByIdAndUserId(int paymentId, long userId)
        {
            return await _paymentRepository.GetPaymentByIdAndUserId(paymentId, userId);
        }

        public async Task<bool> InsertPaymentHistory(int paymentId, PaymentStatusEnum paymentStatusEnum)
        {
            var paymentHistory = await _paymentHistoryRepository.Create(paymentId, (int)paymentStatusEnum);

            if (paymentHistory is null)
                return false;

            return true;
        }

        public async Task<PaymentHistory?> GetPaymentHistoryByPaymentId(int paymentId)
        {
            return await _paymentHistoryRepository.GetPaymentHistoryByPaymentId(paymentId);
        }

        public async Task<bool> InsertPaymentProduct(int productId, int paymentId)
        {
            var paymentProduct = await _paymentProductRepository.Create(productId, paymentId);

            if (paymentProduct is null)
                return false;

            return true;
        }

        public async Task<PaymentProduct?> GetPaymentProductByPaymentId(int paymentId)
        {
            return await _paymentProductRepository.GetPaymentProductByPaymentId(paymentId);
        }

        public async Task<Product?> GetSignature()
        {
            return await _productRepository.GetSignature();
        }

        public async Task<List<Product>> GetProductsByUserId(long userId)
        {
            return await _productRepository.GetProductsByUserId(userId);
        }

        public async Task<PaymentsStatus?> GetPaymentStatus(PaymentStatusEnum paymentStatusEnum)
        {
            return await _paymentStatusRepository.GetPaymentStatusById((int)paymentStatusEnum);
        }

        public async Task<PaymentType?> GetPaymentType(PaymentTypeEnum paymentTypeEnum)
        {
            return await _paymentTypeRepository.GetPaymentTypeById((int)paymentTypeEnum);
        }
    }
}
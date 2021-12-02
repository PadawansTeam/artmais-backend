using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Mail.Factories;
using ArtmaisBackend.Core.Mail.Requests;
using ArtmaisBackend.Core.Mail.Services;
using ArtmaisBackend.Core.Payments.Enums;
using ArtmaisBackend.Core.Payments.Interface;
using ArtmaisBackend.Core.Payments.Request;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using MercadoPago.Client;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;
using Microsoft.Extensions.Configuration;
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
                              ISignatureRepository signatureRepository,
                              IConfiguration configuration,
                              IMercadoPagoPaymentClient mercadoPagoPaymentClient,
                              IMailService mailService)
        {
            _paymentHistoryRepository = paymentHistoryRepository;
            _paymentProductRepository = paymentProductRepository;
            _paymentRepository = paymentRepository;
            _paymentStatusRepository = paymentStatusRepository;
            _paymentTypeRepository = paymentTypeRepository;
            _productRepository = productRepository;
            _signatureRepository = signatureRepository;
            _token = configuration["MERCADO_PAGO_TOKEN"];
            _mercadoPagoPaymentClient = mercadoPagoPaymentClient;
            _mailService = mailService;
        }

        private readonly IPaymentHistoryRepository _paymentHistoryRepository;
        private readonly IPaymentProductRepository _paymentProductRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISignatureRepository _signatureRepository;
        private readonly string _token;
        private readonly IMercadoPagoPaymentClient _mercadoPagoPaymentClient;
        private readonly IMailService _mailService;

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
                    Email = paymentRequest.Email
                },
                NotificationUrl = PaymentDefaults.NOTIFICATION_URL
            };

            var requestOptions = new RequestOptions
            {
                AccessToken = _token
            };

            Payment payment = await _mercadoPagoPaymentClient.CreateAsync(request, requestOptions);

            var paymentInfo = await InsertPayment(userId, PaymentTypeEnum.CREDIT, payment.Id, paymentRequest.Email).ConfigureAwait(false);

            await InsertPaymentHistory(paymentInfo.PaymentID, PaymentStatusEnum.CREATED).ConfigureAwait(false);

            var emailRequest = new EmailRequest
            {
                ToEmail = paymentRequest.Email,
                Subject = PaymentDefaults.PAYMENT_CREATED_SUBJECT,
                Body = BodyFactory.PaymentBody(PaymentDefaults.PAYMENT_CREATED_MESSAGE)
            };

            await _mailService.SendEmailAsync(emailRequest);

            var userSignature = await _signatureRepository.GetSignatureByUserId(userId);

            if (userSignature != null)
            {
                userSignature.EndDate = userSignature.EndDate.AddYears(1);
                await _signatureRepository.Update(userSignature);

                var signatureProduct = await GetSignature().ConfigureAwait(false);
                await InsertPaymentProduct(signatureProduct.ProductID, paymentInfo.PaymentID).ConfigureAwait(false);

                return payment;
            }

            await _signatureRepository.Create(userId);

            var product = await GetSignature().ConfigureAwait(false);
            await InsertPaymentProduct(product.ProductID, paymentInfo.PaymentID).ConfigureAwait(false);

            return payment;
        }

        public async Task<Payment> UpdatePaymentAsync(long id)
        {
            var requestOptions = new RequestOptions 
            {
                AccessToken = _token    
            };

            Payment payment = await _mercadoPagoPaymentClient.GetAsync(id, requestOptions);

            var userPayment = await _paymentRepository.GetPaymentsByExternalPaymentId(id);

            var emailRequest = new EmailRequest
            {
                ToEmail = userPayment.PaymentEmail
            };

            await UpdatePayment(userPayment).ConfigureAwait(false);

            if (payment.Status == "approved")
            {
                await InsertPaymentHistory(userPayment.PaymentID, PaymentStatusEnum.DONE).ConfigureAwait(false);

                emailRequest.Subject = PaymentDefaults.PAYMENT_DONE_SUBJECT;
                emailRequest.Body = BodyFactory.PaymentBody(PaymentDefaults.PAYMENT_DONE_MESSAGE);

                await _mailService.SendEmailAsync(emailRequest);
            }

            if (payment.Status == "in_process")
            {
                await InsertPaymentHistory(userPayment.PaymentID, PaymentStatusEnum.PROCESSING).ConfigureAwait(false);

                emailRequest.Subject = PaymentDefaults.PAYMENT_PROCESSING_SUBJECT;
                emailRequest.Body = BodyFactory.PaymentBody(PaymentDefaults.PAYMENT_PROCESSING_MESSAGE);

                await _mailService.SendEmailAsync(emailRequest);
            }

            if (payment.Status == "rejected")
            {
                await InsertPaymentHistory(userPayment.PaymentID, PaymentStatusEnum.UNDONE).ConfigureAwait(false);

                emailRequest.Subject = PaymentDefaults.PAYMENT_UNDONE_SUBJECT;
                emailRequest.Body = BodyFactory.PaymentBody(PaymentDefaults.PAYMENT_UNDONE_MESSAGE);

                await _mailService.SendEmailAsync(emailRequest);
            }

            return payment;
        }

        private async Task<Entities.Payments?> InsertPayment(long userId, PaymentTypeEnum paymentTypeEnum, long? externalPaymentId, string paymentEmail)
        {
            var payment = await _paymentRepository.Create(userId, (int)paymentTypeEnum, externalPaymentId, paymentEmail);

            return payment;
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
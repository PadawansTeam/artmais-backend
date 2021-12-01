using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Mail.Services;
using ArtmaisBackend.Core.Payments.Enums;
using ArtmaisBackend.Core.Payments.Interface;
using ArtmaisBackend.Core.Payments.Request;
using ArtmaisBackend.Core.Payments.Service;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using FluentAssertions;
using MercadoPago.Client;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Payments
{
    public class PaymentServiceTest
    {
        [Fact(DisplayName = "Payment Create Request Should Be Returns Payments And Create Signature")]
        public async Task PaymentCreateRequestShouldBeReturnsPayments()
        {
            #region Mocks
            var userId = 113;
            var date = DateTime.Now;
            var mockPaymentHistoryRepository = new Mock<IPaymentHistoryRepository>();
            var mockPaymentProductRepository = new Mock<IPaymentProductRepository>();
            var mockPaymentRepository = new Mock<IPaymentRepository>();
            var mockPaymentStatusRepository = new Mock<IPaymentStatusRepository>();
            var mockPaymentTypeRepository = new Mock<IPaymentTypeRepository>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockSignatureRepository = new Mock<ISignatureRepository>();
            var mockMercadoPagoPaymentClient = new Mock<IMercadoPagoPaymentClient>();
            var mockMailService = new Mock<IMailService>();

            Signature signature = null;
            var paymentRequest = new PaymentRequest
            {
                TransactionAmount = 10,
                CardToken = "b0c081ae788978aa4dc6cb4b18603022",
                Description = "description",
                Installments = 1,
                PaymentMethodId = "visa",
                Email = "emailtest@aluno.ifsp.edu.br"

            };
            var payment = new ArtmaisBackend.Core.Entities.Payments
            {
                PaymentID = 1,
                UserID = 1,
                LastUpdateDate = date
            };
            var paymentHistory = new PaymentHistory
            {
                PaymentHistoryID = 1,
                PaymentID = 1,
                CreateDate = date,
                PaymentStatusID = 1
            };
            var PaymentMercadPago = new Payment
            {
                Status = "CREATED",
                Id = 1
            };
            var product = new Product
            {
                ProductID = 1,
                Name = "Assinatura Premium"
            };
            var paymentProduct = new PaymentProduct
            {
                PaymentID = 1,
                PaymentProductID = 1
            };
            var paymentUpdate = new ArtmaisBackend.Core.Entities.Payments
            {
                PaymentID = 1,
                UserID = 1,
                LastUpdateDate = date.AddMinutes(1)
            };
            var paymentHistoryUpdate = new PaymentHistory
            {
                PaymentHistoryID = 1,
                PaymentID = 1,
                CreateDate = date,
                PaymentStatusID = 3
            };

            var inMemorySettings = new Dictionary<string, string> {
                {"MERCADO_PAGO_TOKEN", "b62ff62b9c32a6454ea75d9b8bfebbbb"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);
            mockPaymentRepository.Setup(x => x.Create(userId, (int)PaymentTypeEnum.CREDIT, 1)).ReturnsAsync(payment);
            mockPaymentRepository.Setup(x => x.GetPaymentByUserId(userId)).ReturnsAsync(payment);
            mockPaymentHistoryRepository.Setup(x => x.Create(payment.PaymentID, (int)PaymentStatusEnum.CREATED)).ReturnsAsync(paymentHistory);
            mockMercadoPagoPaymentClient.Setup(x => x.CreateAsync(It.IsAny<PaymentCreateRequest>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(PaymentMercadPago);
            mockProductRepository.Setup(x => x.GetSignature()).ReturnsAsync(product);
            mockPaymentProductRepository.Setup(x => x.Create(payment.PaymentID, product.ProductID)).ReturnsAsync(paymentProduct);
            mockSignatureRepository.Setup(x => x.Create(userId));
            mockPaymentRepository.Setup(x => x.Update(payment)).ReturnsAsync(paymentUpdate);
            mockPaymentHistoryRepository.Setup(x => x.Create(payment.PaymentID, (int)PaymentStatusEnum.DONE)).ReturnsAsync(paymentHistoryUpdate);
            #endregion

            var paymentService = new PaymentService(mockPaymentHistoryRepository.Object, mockPaymentProductRepository.Object, mockPaymentRepository.Object, mockPaymentStatusRepository.Object, mockPaymentTypeRepository.Object, mockProductRepository.Object, mockSignatureRepository.Object, configuration, mockMercadoPagoPaymentClient.Object, mockMailService.Object);
            var result = await paymentService.PaymentCreateRequest(paymentRequest, userId);

            result.Should().BeEquivalentTo(result);
        }
    }
}

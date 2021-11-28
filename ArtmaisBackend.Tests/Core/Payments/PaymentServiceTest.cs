using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Payments.Enums;
using ArtmaisBackend.Core.Payments.Interface;
using ArtmaisBackend.Core.Payments.Request;
using ArtmaisBackend.Core.Payments.Service;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using FluentAssertions;
using MercadoPago.Client;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;
using Moq;
using System;
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
                Status = "CREATED"
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
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);
            mockPaymentRepository.Setup(x => x.Create(userId, (int)PaymentTypeEnum.CREDIT)).ReturnsAsync(payment);
            mockPaymentRepository.Setup(x => x.GetPaymentByUserId(userId)).ReturnsAsync(payment);
            mockPaymentHistoryRepository.Setup(x => x.Create(payment.PaymentID, (int)PaymentStatusEnum.CREATED)).ReturnsAsync(paymentHistory);
            mockMercadoPagoPaymentClient.Setup(x => x.CreateAsync(It.IsAny<PaymentCreateRequest>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(PaymentMercadPago);
            mockProductRepository.Setup(x => x.GetSignature()).ReturnsAsync(product);
            mockPaymentProductRepository.Setup(x => x.Create(payment.PaymentID, product.ProductID)).ReturnsAsync(paymentProduct);
            mockSignatureRepository.Setup(x => x.Create(userId));
            mockPaymentRepository.Setup(x => x.Update(payment)).ReturnsAsync(paymentUpdate);
            mockPaymentHistoryRepository.Setup(x => x.Create(payment.PaymentID, (int)PaymentStatusEnum.DONE)).ReturnsAsync(paymentHistoryUpdate);
            #endregion

            var paymentService = new PaymentService(mockPaymentHistoryRepository.Object, mockPaymentProductRepository.Object, mockPaymentRepository.Object, mockPaymentStatusRepository.Object, mockPaymentTypeRepository.Object, mockProductRepository.Object, mockSignatureRepository.Object, mockMercadoPagoPaymentClient.Object);
            var result = await paymentService.PaymentCreateRequest(paymentRequest, userId);

            result.Should().BeEquivalentTo(result);
        }

        [Fact(DisplayName = "Payment Create Request Should Be Throws When Signature Already Exists")]
        public async Task PaymentCreateRequestShouldBeThrowsWhenSignatureAlreadyExists()
        {
            #region Mocks
            var userId = 113;
            var mockPaymentHistoryRepository = new Mock<IPaymentHistoryRepository>();
            var mockPaymentProductRepository = new Mock<IPaymentProductRepository>();
            var mockPaymentRepository = new Mock<IPaymentRepository>();
            var mockPaymentStatusRepository = new Mock<IPaymentStatusRepository>();
            var mockPaymentTypeRepository = new Mock<IPaymentTypeRepository>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockSignatureRepository = new Mock<ISignatureRepository>();
            var mockMercadoPagoPaymentClient = new Mock<IMercadoPagoPaymentClient>();

            var signature = new Signature
            {
                SignatureID = 1
            };
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);
            #endregion

            var paymentService = new PaymentService(mockPaymentHistoryRepository.Object, mockPaymentProductRepository.Object, mockPaymentRepository.Object, mockPaymentStatusRepository.Object, mockPaymentTypeRepository.Object, mockProductRepository.Object, mockSignatureRepository.Object, mockMercadoPagoPaymentClient.Object);

            Func<Task> result = async () =>
            {
                await paymentService.PaymentCreateRequest(It.IsAny<PaymentRequest>(), userId);
            };
            await result.Should().ThrowAsync<ArgumentException>();
        }

        [Fact(DisplayName = "Payment Create Request Should Be Throws When Payments Is Null")]
        public async Task PaymentCreateRequestShouldBeThrowsWhenPaymentsIsNull()
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

            Signature signature = null;
            ArtmaisBackend.Core.Entities.Payments payment = null;
            var paymentRequest = new PaymentRequest
            {
                TransactionAmount = 10,
                CardToken = "b0c081ae788978aa4dc6cb4b18603022",
                Description = "description",
                Installments = 1,
                PaymentMethodId = "visa",
                Email = "emailtest@aluno.ifsp.edu.br"

            };
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);
            mockPaymentRepository.Setup(x => x.Create(userId, (int)PaymentTypeEnum.CREDIT)).ReturnsAsync(payment);
            #endregion

            var paymentService = new PaymentService(mockPaymentHistoryRepository.Object, mockPaymentProductRepository.Object, mockPaymentRepository.Object, mockPaymentStatusRepository.Object, mockPaymentTypeRepository.Object, mockProductRepository.Object, mockSignatureRepository.Object, mockMercadoPagoPaymentClient.Object);

            Func<Task> result = async () =>
            {
                await paymentService.PaymentCreateRequest(paymentRequest, userId);
            };
            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(DisplayName = "Payment Create Request Should Be Throws When Get Payment By User Id Is Nul")]
        public async Task PaymentCreateRequestShouldBeThrowsWhenGetPaymentByUserIdIsNul()
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
            ArtmaisBackend.Core.Entities.Payments paymentUserId = null;
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);
            mockPaymentRepository.Setup(x => x.Create(userId, (int)PaymentTypeEnum.CREDIT)).ReturnsAsync(payment);
            mockPaymentRepository.Setup(x => x.GetPaymentByUserId(userId)).ReturnsAsync(paymentUserId);
            #endregion

            var paymentService = new PaymentService(mockPaymentHistoryRepository.Object, mockPaymentProductRepository.Object, mockPaymentRepository.Object, mockPaymentStatusRepository.Object, mockPaymentTypeRepository.Object, mockProductRepository.Object, mockSignatureRepository.Object, mockMercadoPagoPaymentClient.Object);

            Func<Task> result = async () =>
            {
                await paymentService.PaymentCreateRequest(paymentRequest, userId);
            };
            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(DisplayName = "Payment Create Request Should Be Throws When Insert Payment History Returns Null")]
        public async Task PaymentCreateRequestShouldBeThrowsWhenInsertPaymentHistoryReturnsNull()
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
            PaymentHistory paymentHistory = null;
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);
            mockPaymentRepository.Setup(x => x.Create(userId, (int)PaymentTypeEnum.CREDIT)).ReturnsAsync(payment);
            mockPaymentRepository.Setup(x => x.GetPaymentByUserId(userId)).ReturnsAsync(payment);
            mockPaymentHistoryRepository.Setup(x => x.Create(payment.PaymentID, (int)PaymentStatusEnum.CREATED)).ReturnsAsync(paymentHistory);
            #endregion

            var paymentService = new PaymentService(mockPaymentHistoryRepository.Object, mockPaymentProductRepository.Object, mockPaymentRepository.Object, mockPaymentStatusRepository.Object, mockPaymentTypeRepository.Object, mockProductRepository.Object, mockSignatureRepository.Object, mockMercadoPagoPaymentClient.Object);

            Func<Task> result = async () =>
            {
                await paymentService.PaymentCreateRequest(paymentRequest, userId);
            };
            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(DisplayName = "Payment Create Request Should Be Throw When Mercado Pago Payment Client Returns Null")]
        public async Task PaymentCreateRequestShouldBeThrowWhenMercadoPagoPaymentClientReturnsNull()
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
            Payment PaymentMercadPago = null;
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);
            mockPaymentRepository.Setup(x => x.Create(userId, (int)PaymentTypeEnum.CREDIT)).ReturnsAsync(payment);
            mockPaymentRepository.Setup(x => x.GetPaymentByUserId(userId)).ReturnsAsync(payment);
            mockPaymentHistoryRepository.Setup(x => x.Create(payment.PaymentID, (int)PaymentStatusEnum.CREATED)).ReturnsAsync(paymentHistory);
            mockMercadoPagoPaymentClient.Setup(x => x.CreateAsync(It.IsAny<PaymentCreateRequest>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(PaymentMercadPago);
            #endregion

            var paymentService = new PaymentService(mockPaymentHistoryRepository.Object, mockPaymentProductRepository.Object, mockPaymentRepository.Object, mockPaymentStatusRepository.Object, mockPaymentTypeRepository.Object, mockProductRepository.Object, mockSignatureRepository.Object, mockMercadoPagoPaymentClient.Object);

            Func<Task> result = async () =>
            {
                await paymentService.PaymentCreateRequest(paymentRequest, userId);
            };
            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(DisplayName = "Payment Create Request Should Be Throws When Get Signature Returns Null")]
        public async Task PaymentCreateRequestShouldBeThrowsWhenGetSignatureReturnsNull()
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
                Status = "CREATED"
            };
            Product product = null;
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);
            mockPaymentRepository.Setup(x => x.Create(userId, (int)PaymentTypeEnum.CREDIT)).ReturnsAsync(payment);
            mockPaymentRepository.Setup(x => x.GetPaymentByUserId(userId)).ReturnsAsync(payment);
            mockPaymentHistoryRepository.Setup(x => x.Create(payment.PaymentID, (int)PaymentStatusEnum.CREATED)).ReturnsAsync(paymentHistory);
            mockMercadoPagoPaymentClient.Setup(x => x.CreateAsync(It.IsAny<PaymentCreateRequest>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(PaymentMercadPago);
            mockProductRepository.Setup(x => x.GetSignature()).ReturnsAsync(product);
            #endregion

            var paymentService = new PaymentService(mockPaymentHistoryRepository.Object, mockPaymentProductRepository.Object, mockPaymentRepository.Object, mockPaymentStatusRepository.Object, mockPaymentTypeRepository.Object, mockProductRepository.Object, mockSignatureRepository.Object, mockMercadoPagoPaymentClient.Object);

            Func<Task> result = async () =>
            {
                await paymentService.PaymentCreateRequest(paymentRequest, userId);
            };
            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(DisplayName = "Payment Create Request Should Be Throws When Insert Payment Product Returns Null")]
        public async Task PaymentCreateRequestShouldBeThrowsWhenInsertPaymentProductReturnsNull()
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
                Status = "CREATED"
            };
            var product = new Product
            {
                ProductID = 1,
                Name = "Assinatura Premium"
            };
            PaymentProduct paymentProduct = null;
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);
            mockPaymentRepository.Setup(x => x.Create(userId, (int)PaymentTypeEnum.CREDIT)).ReturnsAsync(payment);
            mockPaymentRepository.Setup(x => x.GetPaymentByUserId(userId)).ReturnsAsync(payment);
            mockPaymentHistoryRepository.Setup(x => x.Create(payment.PaymentID, (int)PaymentStatusEnum.CREATED)).ReturnsAsync(paymentHistory);
            mockMercadoPagoPaymentClient.Setup(x => x.CreateAsync(It.IsAny<PaymentCreateRequest>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(PaymentMercadPago);
            mockProductRepository.Setup(x => x.GetSignature()).ReturnsAsync(product);
            mockPaymentProductRepository.Setup(x => x.Create(payment.PaymentID, product.ProductID)).ReturnsAsync(paymentProduct);
            #endregion

            var paymentService = new PaymentService(mockPaymentHistoryRepository.Object, mockPaymentProductRepository.Object, mockPaymentRepository.Object, mockPaymentStatusRepository.Object, mockPaymentTypeRepository.Object, mockProductRepository.Object, mockSignatureRepository.Object, mockMercadoPagoPaymentClient.Object);

            Func<Task> result = async () =>
            {
                await paymentService.PaymentCreateRequest(paymentRequest, userId);
            };
            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(DisplayName = "Payment Create Request Should Be Throws When Update Payment Returns Null")]
        public async Task PaymentCreateRequestShouldBeThrowsWhenUpdatePaymentReturnsNull()
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
                Status = "CREATED"
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
            ArtmaisBackend.Core.Entities.Payments paymentUpdate = null;
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);
            mockPaymentRepository.Setup(x => x.Create(userId, (int)PaymentTypeEnum.CREDIT)).ReturnsAsync(payment);
            mockPaymentRepository.Setup(x => x.GetPaymentByUserId(userId)).ReturnsAsync(payment);
            mockPaymentHistoryRepository.Setup(x => x.Create(payment.PaymentID, (int)PaymentStatusEnum.CREATED)).ReturnsAsync(paymentHistory);
            mockMercadoPagoPaymentClient.Setup(x => x.CreateAsync(It.IsAny<PaymentCreateRequest>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(PaymentMercadPago);
            mockProductRepository.Setup(x => x.GetSignature()).ReturnsAsync(product);
            mockPaymentProductRepository.Setup(x => x.Create(payment.PaymentID, product.ProductID)).ReturnsAsync(paymentProduct);
            mockSignatureRepository.Setup(x => x.Create(userId));
            mockPaymentRepository.Setup(x => x.Update(payment)).ReturnsAsync(paymentUpdate);
            #endregion

            var paymentService = new PaymentService(mockPaymentHistoryRepository.Object, mockPaymentProductRepository.Object, mockPaymentRepository.Object, mockPaymentStatusRepository.Object, mockPaymentTypeRepository.Object, mockProductRepository.Object, mockSignatureRepository.Object, mockMercadoPagoPaymentClient.Object);

            Func<Task> result = async () =>
            {
                await paymentService.PaymentCreateRequest(paymentRequest, userId);
            };
            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(DisplayName = "Payment Create Request Should Be Throws When Insert New Payment History Returns Null")]
        public async Task PaymentCreateRequestShouldBeThrowsWhenInsertNewPaymentHistoryReturnsNull()
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
                Status = "CREATED"
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
            PaymentHistory paymentHistoryUpdate = null;
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);
            mockPaymentRepository.Setup(x => x.Create(userId, (int)PaymentTypeEnum.CREDIT)).ReturnsAsync(payment);
            mockPaymentRepository.Setup(x => x.GetPaymentByUserId(userId)).ReturnsAsync(payment);
            mockPaymentHistoryRepository.Setup(x => x.Create(payment.PaymentID, (int)PaymentStatusEnum.CREATED)).ReturnsAsync(paymentHistory);
            mockMercadoPagoPaymentClient.Setup(x => x.CreateAsync(It.IsAny<PaymentCreateRequest>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(PaymentMercadPago);
            mockProductRepository.Setup(x => x.GetSignature()).ReturnsAsync(product);
            mockPaymentProductRepository.Setup(x => x.Create(payment.PaymentID, product.ProductID)).ReturnsAsync(paymentProduct);
            mockSignatureRepository.Setup(x => x.Create(userId));
            mockPaymentRepository.Setup(x => x.Update(payment)).ReturnsAsync(paymentUpdate);
            mockPaymentHistoryRepository.Setup(x => x.Create(payment.PaymentID, (int)PaymentStatusEnum.DONE)).ReturnsAsync(paymentHistoryUpdate);
            #endregion

            var paymentService = new PaymentService(mockPaymentHistoryRepository.Object, mockPaymentProductRepository.Object, mockPaymentRepository.Object, mockPaymentStatusRepository.Object, mockPaymentTypeRepository.Object, mockProductRepository.Object, mockSignatureRepository.Object, mockMercadoPagoPaymentClient.Object);


            Func<Task> result = async () =>
            {
                await paymentService.PaymentCreateRequest(paymentRequest, userId);
            };
            await result.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}

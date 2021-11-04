using ArtmaisBackend.Core.Payment.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using MercadoPago.Client;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Http;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Payment.Enums;

namespace ArtmaisBackend.Core.Payment.Service
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

        //public async Task InsertMercadoPagoRequest()
        //{

        //    MercadoPagoConfig.AccessToken = "TEST-4734890284706792-101621-772162e631cd84775da9d50f2e0acb43-278907011";

        //    var request = new PaymentCreateRequest
        //    {
        //        TransactionAmount = 10,
        //        Token = "CARD_TOKEN",
        //        Description = "Payment description",
        //        Installments = 1,
        //        PaymentMethodId = "visa",
        //        Payer = new PaymentPayerRequest
        //        {
        //            Email = "test.payer@email.com",
        //        }
        //    };

        //    var client = new PaymentClient();
        //    var requestOptions = new RequestOptions();
        //    requestOptions.AccessToken = "YOUR_ACCESS_TOKEN";

        //    Payment payment = await client.CreateAsync(request, requestOptions);

        //    var handler = new HttpClientHandler
        //    {
        //        Proxy = new WebProxy(proxyUrl),
        //        UseProxy = true,
        //    };
        //    var httpClient = new HttpClient(handler);
        //    MercadoPagoConfig.HttpClient = new DefaultHttpClient(httpClient);
        //}


        public async Task<bool> InsertPayment(long userId, PaymentStatusEnum PaymentStatusEnum)
        {
            var payment = await _paymentRepository.Create(userId, (int)PaymentStatusEnum);

            if (payment is null)
                return false;

            return true;
        }
    }
}

using ArtmaisBackend.Core.Payment.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;

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
            this._paymentHistoryRepository = paymentHistoryRepository;
            this._paymentProductRepository = paymentProductRepository;
            this._paymentRepository = paymentRepository;
            this._paymentStatusRepository = paymentStatusRepository;
            this._paymentTypeRepository = paymentTypeRepository;
            this._productRepository = productRepository;
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        private readonly IPaymentHistoryRepository _paymentHistoryRepository;
        private readonly IPaymentProductRepository _paymentProductRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentStatusRepository _paymentStatusRepository;
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

    }
}

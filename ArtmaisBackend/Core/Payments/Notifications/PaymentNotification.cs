using System;

namespace ArtmaisBackend.Core.Payments.Notifications
{
    public class PaymentNotification
    {
        public int Id { get; set; }
        public bool LiveMode { get; set; }
        public string Type { get; set; }
        public DateTime DateCreated { get; set; }
        public long ApplicationId { get; set; }
        public long UserId { get; set; }
        public int Version { get; set; }
        public string ApiVersion { get; set; }
        public string Action { get; set; }
        public Data Data { get; set; }
    }
}

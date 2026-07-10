namespace DTO.Models.Auth
{
    public class SMSSettings
    {
        public string User { get; set; }
        public string Key { get; set; }
        public string SenderId { get; set; }
        public string Url { get; set; }
        public string EntityId { get; set; }
        public string TG_BookingConfirmation { get; set; }
        public string TG_Login { get; set; }
        public string TG_Sign_up { get; set; }
        public string TG_Feedback { get; set; }
        public string TG_CallBusy_Revised { get; set; }
        public string TG_PaymentFailure { get; set; }
        public string TG_CancellationBySP { get; set; }
        public string TG_BookingCancellation { get; set; }
        public string TG_Enquire { get; set; }
        public string TG_PhoneNumberVerifyOTP { get; set; }
    }

}
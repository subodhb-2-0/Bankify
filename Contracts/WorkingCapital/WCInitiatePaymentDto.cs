namespace Contracts.WorkingCapital
{
    public class WCInitiatePaymentDto
    {
        public long p_orgid { get; set; }
        public int p_paymentmode { get; set; } // integer, 
        public double p_amount { get; set; } //  numeric, 
        public int p_bankid { get; set; } //  integer, 
        public int p_pgid { get; set; } //  integer,
        public string p_bankaccount { get; set; } //  character varying, 
        public int p_status { get; set; } //  integer,
        public string p_remark { get; set; } //  character varying, 
        public int p_issueingbankid { get; set; } //  integer,
        public int p_creator { get; set; } //  integer
        public string p_bankpayinslip { get; set; } //  character varying,
        public string p_instrumentnumber { get; set; } //  character varying, 
        public string p_issuingifsccode { get; set; } //  character varying, 
        public string p_vpa { get; set; } //  character varying
        public string? p_fileFormat { get; set; }
        public string p_depositDate { get; set; }
        public string p_depositTime { get; set; }
    }
}

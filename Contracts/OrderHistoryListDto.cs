namespace Contracts
{
    public class OrderHistoryListDto
    {
        public long p_orgid {  get; set; }
        public string p_fromdate {  get; set; }
        public string p_todate {  get; set; }
        public int p_offsetrows {  get; set; }
        public int p_fetchrows {  get; set; }
        public int p_orderstatus {  get; set; }
    }
}

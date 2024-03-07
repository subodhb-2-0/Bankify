namespace Contracts.Report
{
    public class BalanceTransferRequestDto: LedgerRequestDto
    {
        public int searchById { get; set; }
        public int searchByName { get; set; }
    }
}
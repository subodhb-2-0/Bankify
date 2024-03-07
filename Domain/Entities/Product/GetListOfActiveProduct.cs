namespace Domain.Entities.Product
{
    public class GetListOfActiveProduct
    {
        public int? productid { get; set; }
        public string orgcode {  get; set; }
        public string orgname { get; set; }
        public string productname { get; set; }
        public int status { get; set; }
   
    }
}

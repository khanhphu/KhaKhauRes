namespace KhaKhau.Models;

   
        public record TopSoldProductModel(int Id,string Name, string ImageUrl,int TotalUnitSold);
        public record TopSoldProductByDate(DateTime StartDate, DateTime EndDate, IEnumerable<TopSoldProductModel> TopSoldProducts);
    

namespace Demo.PL.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
    
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

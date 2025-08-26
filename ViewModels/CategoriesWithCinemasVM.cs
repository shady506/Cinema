namespace Cinema.ViewModels
{
    public class CategoriesWithCinemasVM
    {
        public List<Categories> Categories { get; set; } = null!;
        public List<Cinemas> Cinemas{ get; set; } = null!;
        public Movies? Movie { get; set; }
    }
}

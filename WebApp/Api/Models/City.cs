namespace Api.Models
{
    public class City
    {
        public City(string cityName, string stateName)
        {
            CityName = cityName;
            StateName = stateName;
        }
        public int Id { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
    }
}

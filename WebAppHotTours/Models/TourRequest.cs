using Hot_Tours_BL.Models;
namespace WebAppHotTours.Models
{
    public class TourRequest
    {
        public Direction Direction { get; set; }
        public DateTime Date { get; set; }
        public uint AmountOfDays { get; set; }
        public decimal PriceForMan { get; set; }
        public uint AmountOfMan { get; set; }
        public bool WiFi { get; set; }
        public decimal Surcharge { get; set; }

        public void Check()
        {
            Random rnd = new Random();
            if (AmountOfDays <= 0 || AmountOfDays > 31)
            {
                AmountOfDays = (uint)rnd.Next(1, 31);
            }

            if (PriceForMan < 500 || PriceForMan > 5000)
            {
                PriceForMan = rnd.Next(500, 5000);
            }

            if (AmountOfMan <= 0 || AmountOfMan > 20)
            {
                AmountOfMan = (uint)rnd.Next(1, 20);
            }

            if ((int)Direction > 4 || (int)Direction < 0)
            {
                Direction = Direction.Crimea;
            }

            if (Surcharge < 0)
            {
                Surcharge = rnd.Next(0, 20000);
            }
        }
    }
}

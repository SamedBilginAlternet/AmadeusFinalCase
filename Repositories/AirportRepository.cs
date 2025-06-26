using AmadeusFlightApý.Data;
using AmadeusFlightApý.Models;

namespace AmadeusFlightApý.Repositories
{
    public class AirportRepository : GenericRepository<Airport>, IAirportRepository
    {
        public AirportRepository(ApplicationDbContext context) : base(context) { }
        // Airport'a özel ek metotlar buraya eklenebilir
    }
}

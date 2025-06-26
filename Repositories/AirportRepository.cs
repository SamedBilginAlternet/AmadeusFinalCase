using AmadeusFlightAp�.Data;
using AmadeusFlightAp�.Models;

namespace AmadeusFlightAp�.Repositories
{
    public class AirportRepository : GenericRepository<Airport>, IAirportRepository
    {
        public AirportRepository(ApplicationDbContext context) : base(context) { }
        // Airport'a �zel ek metotlar buraya eklenebilir
    }
}

using AmadeusFlightApý.Data;
using AmadeusFlightApý.Models;

namespace AmadeusFlightApý.Repositories
{
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        public FlightRepository(ApplicationDbContext context) : base(context) { }
        // Flight'a özel ek metotlar buraya eklenebilir
    }
}

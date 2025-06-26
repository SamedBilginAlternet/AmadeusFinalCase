using AmadeusFlightAp�.Data;
using AmadeusFlightAp�.Models;

namespace AmadeusFlightAp�.Repositories
{
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        public FlightRepository(ApplicationDbContext context) : base(context) { }
        // Flight'a �zel ek metotlar buraya eklenebilir
    }
}

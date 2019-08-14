using System.Linq;
using WebApplicationExercise.DataLayer.Models;

namespace WebApplicationExercise.Core
{
    public static class OrderTotalUpdate
    {
        public static void UpdateTotal(this Order order)
        {
            order.Total = order.Products.Sum(x => x.Price * x.Quantity);
        }
    }
}
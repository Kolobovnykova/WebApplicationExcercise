namespace WebApplicationExercise.Core
{
    public class CustomerManager
    {
        public bool IsCustomerVisible(string customerName)
        {
            return customerName != "Hidden Joe";
        }
    }
}
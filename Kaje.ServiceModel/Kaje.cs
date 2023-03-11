using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaje.ServiceModel
{
    [Route("/api/kaje/sort")]
    public class KajeSort : IReturn<List<string>>
    {
        public List<string> Unsorted { get; set; }
    }

    public abstract class KajeBaseItem<T>
    {
        public string Zid { get; set; }
        public T Data { get; set; }
    }

    [Route("/api/kaje/item-add")]
    public class KajeAddItem : Item, IReturn<KajeItem>
    {
    }

    [Route("/api/kaje/items")]
    public class KajeQueryItems : IReturn<List<KajeItem>> { }

    public class KajeItem : KajeBaseItem<Item>
    {

    }

    public class Item
    {
        [ValidateNotEmpty]
        public string Address { get; set; }
        [ValidateNotEmpty]
        public string City { get; set; }
        [ValidateNotEmpty]
        public string State { get; set; }
        [ValidateNotEmpty]
        [ValidateRegularExpression("^[0-9]{5}$", Message ="Postal code should be 5 digits")]
        public string PostalCode { get; set; }
    }

    public class KajeResponse<T>
    {
        public bool[] auth { get; set; }
        public int hitCount { get; set; }
        public bool isCache { get; set; }
        public int life { get; set; }
        public Messages messages { get; set; }
        public int originTs { get; set; }
        public string requestHash { get; set; }
        public T response { get; set; }
        
        public bool success { get; set; }
        public Timers timers { get; set; }
        public int ts { get; set; }
        public string version { get; set; }
    }

    public class Messages
    {
    }

    public class Timers
    {
        public float totality { get; set; }
    }

}

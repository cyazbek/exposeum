using Android.Graphics;

namespace Exposeum.TempModels
{
    public class Floor
    {
        public int Id { get; set; }
        public string Plan { get; set; } 
        public Paint Paint { get; set; }

        public Floor()
        {
            Paint = new Paint(); 
        }

        public override bool Equals(object obj)
        {
            Floor other = (Floor) obj;
            return Id == other.Id && Plan == other.Plan;
        }
    }
}
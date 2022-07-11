namespace ProjectFacualRecognition.Lib.Models
{
    public class ModelBase
    {
        
         public int Id { get; private set; }
         public ModelBase(int id)
         {
            SetId(id);
         }
         public void SetId(int id)
        {
            Id = id;
        }
    }
}
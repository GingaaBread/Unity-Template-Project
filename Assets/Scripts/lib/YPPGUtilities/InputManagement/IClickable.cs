namespace YPPGUtilities
{
    namespace InputManagement
    {
        /**
         *  <summary>
         *  Equips scripts with the ability to be clicked on, replacing the old OnMouseDown event of the old input system
         *  </summary>
         *  
         *  <example>
         *  public class Enemy : Monobehaviour, IClickable { ... }
         *  </example>
         */
        public interface IClickable
        {
            public void OnClick();
        }
    }
}
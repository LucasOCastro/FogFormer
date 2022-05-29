namespace FogFormer
{
    public class EnableTriggerable : Triggerable
    {
        public override void Trigger()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
using ECS_MONO;

namespace Game.Inventory
{
    internal sealed class SelectedHot : EcsComponent  //Выбранный горячий слот
    {
        protected override void OnRegisterEntity(IEntity entity)
        {
            entity.Get<HotSlot>().SetActive(true);
        }
    }
}
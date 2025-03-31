namespace ECS_MONO
{
    public interface IComponentAdapter
    {
        public void SetComponent(EntityMono entityMono);
        void SilentDestroy();
        virtual uint Order => 0;
    }
}
namespace Hathor
{
    class DefaultBuildingClass : IBuildingClass
    {
        public string ID { get => "Building"; }

        // 名字
        public string Name { get => "Default Building Class"; }

        // 创建实例
        public IBuilding Create(dynamic configs)
        {
            return new DefaultBuilding(this, Util.RandomID());
        }
    }

    class DefaultBuilding : IBuilding
    {
        protected IBuildingClass mCls;

        protected string mID;

        public DefaultBuilding(IBuildingClass cls, string id)
        {
            this.mCls = cls;
            this.mID = id;
        }

        public string ID { get => this.mID; }

        // 名字
        public string Name { get; set; }

        // 描述
        public string Desctiption { get; set; }

        // 对应建筑类
        public IBuildingClass GetClass()
        {
            return this.mCls;
        }

        // 当前建筑成长
        public IBuildingUpgrade GetUpgrade()
        {
            return null;
        }

        // 当前建筑更新
        public void Update()
        {
            // do nothing
        }
    }
}

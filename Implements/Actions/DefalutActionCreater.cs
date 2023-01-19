using System.Collections.Generic;
using System.Linq;


namespace Hathor
{
    class DefaultAtionCreater : IActionCreater
    {
        public string ID { get => "default"; }

        public string Name { get => "Default Action Creater"; }

        public IAction[] CreateByBuilding(IBuilding building)
        {
            return null;
        }

        public IAction[] CreateByCharacter(ICharacter character)
        {
            IEnumerable<IAction> actions = new List<IAction>();
            var abilities = character.GetAbilities();
            if (abilities != null)
            {
                actions = actions.Union(
                    abilities.ListAbilities().Select(
                    ability => new DefaultCharacterAction(character, ability))
                );
            }

            var equipments = character.GetEquipments();
            if (equipments != null)
            {
                foreach (var item in equipments.ListItem())
                {
                    var itemAbilities = item.GetAbilities();
                    if (itemAbilities != null)
                    {
                        actions = actions.Union(
                            itemAbilities.ListAbilities().Select(
                            ability => new DefaultItemAction(item, ability))
                        );
                    }
                }
            }

            return actions.ToArray();
        }

        public IAction[] CreateByItem(IItem item)
        {
            var abilities = item.GetAbilities();
            if (abilities == null)
            {
                return null;
            }

            return abilities.ListAbilities().Select(
                ability => new DefaultItemAction(item, ability)).ToArray();
        }
    }
}

using Game;
using Game.Common;
using Game.Vehicles;
using Unity.Collections;
using Unity.Entities;

namespace MagicTaxi;

public partial class MagicTaxiSystem : GameSystemBase
{
    private EntityQuery m_TaxiQuery;

    protected override void OnCreate()
    {
        base.OnCreate();

        // This is a simpler way to create the same query.
        // It looks for all entities that are a Vehicle and a Taxi,
        // but are not marked as Deleted or Temp.
        m_TaxiQuery = GetEntityQuery(new EntityQueryDesc()
        {
            All = [ComponentType.ReadOnly<Vehicle>(), ComponentType.ReadOnly<Taxi>()],
            None = [ComponentType.ReadOnly<Deleted>(), ComponentType.ReadOnly<Temp>()]
        });

        Mod.log.Info("MagicTaxiSystem is active.");
    }

    protected override void OnUpdate()
    {
        // This is the most important fix.
        // It checks the setting from the options menu. If the box is unchecked, it stops here.
        if (Mod.Settings == null || !Mod.Settings.TaxiRemoverEnabled)
        {
            return;
        }

        // Create a temporary list of taxis to delete.
        using var entitiesToDelete = new NativeList<Entity>(Allocator.Temp);
        
        // Loop through all entities found by our query.
        foreach (var entity in m_TaxiQuery.ToEntityArray(Allocator.Temp))
        {
            // Get the Taxi component data for the current entity.
            var taxiData = EntityManager.GetComponentData<Taxi>(entity);

            // This logic is from the original mod: it only removes taxis coming from outside the city.
            if ((taxiData.m_State & TaxiFlags.FromOutside) != 0)
            {
                entitiesToDelete.Add(entity);
            }
        }

        // If we found any taxis to delete, destroy them all at once.
        if (entitiesToDelete.Length > 0)
        {
            EntityManager.DestroyEntity(entitiesToDelete);
        }
    }
}

using Game;
using Game.Buildings;
using Game.Common;
using Game.Prefabs;
using Game.Routes;
using Game.Simulation;
using Game.Tools;
using Game.Vehicles;
using Unity.Collections;
using Unity.Entities;
using InitializeSystem = Game.Vehicles.InitializeSystem;
using SpawnLocation = Game.Objects.SpawnLocation;
using TakeoffLocation = Game.Routes.TakeoffLocation;
using Taxi = Game.Vehicles.Taxi;
using WorkVehicle = Game.Vehicles.WorkVehicle;

namespace MagicTaxi;


    public partial class MagicTaxiSystem() : GameSystemBase
    {
        private EntityQuery _TaxiRemoverQuery;
        protected override void OnCreate()
        {
            base.OnCreate();

            _TaxiRemoverQuery = GetEntityQuery(new EntityQueryDesc()
            {
                All =
                [
                    ComponentType.ReadOnly<Vehicle>(), 
                    ComponentType.ReadOnly<Taxi>(), 
                ],
                None = 
                [
                    ComponentType.ReadOnly<Deleted>(),
                    ComponentType.ReadOnly<Temp>(),
                ]
            });
        }
        protected override void OnUpdate()
        {
            var taxiRemover = _TaxiRemoverQuery.ToEntityArray((Allocator.Temp));
            foreach (var entity in taxiRemover)
            {
                var taxiflagData = EntityManager.GetComponentData<Taxi>(entity);
                if (((taxiflagData.m_State & TaxiFlags.FromOutside) != 0))
                {
                    try
                    {
                        EntityManager.AddComponent<Deleted>(entity);
                    }
                    catch
                    {
                        //do nothing
                    }
                }

            }
        }
    }
        
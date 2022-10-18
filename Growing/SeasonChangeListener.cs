using Bindito.Core;
using FloodSeason.Events;
using FloodSeason.Seasons.Types;
using Timberborn.SingletonSystem;
using Timberborn.SoilMoistureSystem;

namespace FloodSeason.Growing
{
    public class SeasonChangeListener : ILoadableSingleton
    {
        private EventBus _eventBus;
        private SoilMoistureSimulationSettings _soilMoistureSimulationSettings;

        [Inject]
        public void InjectDependencies(EventBus eventBus, SoilMoistureSimulationSettings soilMoistureSimulationSettings)
        {
            _eventBus = eventBus;
            _soilMoistureSimulationSettings = soilMoistureSimulationSettings;
        }

        public void Load()
        {
            _eventBus.Register(this);
        }

        [OnEvent]
        public void OnSeasonChanged(SeasonChangedEvent seasonChangedEvent)
        {
            if (seasonChangedEvent.Season is Winter)
            {
                _soilMoistureSimulationSettings._maxMoisture = 0;
            }
            else
            {
                _soilMoistureSimulationSettings._maxMoisture = 16;
            }            
        }
    }
}

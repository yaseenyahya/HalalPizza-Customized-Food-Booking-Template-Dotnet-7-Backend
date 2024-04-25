using halalpizzabackend.Data;
using System.Collections.Generic;
using System.Linq;

namespace halalpizzabackend.Models
{
    public class SliderSettingsService : ISliderSettingsService
    {
        private readonly ApplicationDbContext _dbContext; // Replace YourDbContext with the actual DbContext class you are using

        public SliderSettingsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SliderSettings AddSlider(SliderSettings sliderSettings)
        {
            _dbContext.SliderSettings.Add(sliderSettings);
            _dbContext.SaveChanges();
            return sliderSettings;
        }

        public void DeleteSlider(int sliderId)
        {
            var slider = _dbContext.SliderSettings.Find(sliderId);
            if (slider != null)
            {
                _dbContext.SliderSettings.Remove(slider);
                _dbContext.SaveChanges();
            }
        }

        public SliderSettings EditSlider(int sliderId, SliderSettings updatedSliderSettings)
        {
            var existingSlider = _dbContext.SliderSettings.Find(sliderId);

            if (existingSlider != null)
            {
                existingSlider.Path = updatedSliderSettings.Path;
                existingSlider.Link = updatedSliderSettings.Link;
                existingSlider.Enabled = updatedSliderSettings.Enabled;

                _dbContext.SaveChanges();
            }

            return existingSlider;
        }

        public List<SliderSettings> GetAllSliders(bool filterEnabled)
        {
            if(filterEnabled)
            return _dbContext.SliderSettings.Where(s => s.Enabled == SliderStatus.Enabled).OrderByDescending(s => s.ID).ToList();
            else
                return _dbContext.SliderSettings.OrderByDescending(s => s.ID).ToList();

        }

        public SliderSettings GetSliderById(int sliderId)
        {
            // Implement logic to retrieve a user by ID
            return _dbContext.SliderSettings.Find(sliderId);
        }
    }
}

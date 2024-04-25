using halalpizzabackend.Models;

public interface ISliderSettingsService
{
    SliderSettings AddSlider(SliderSettings userDetails);
    void DeleteSlider(int sliderId);
    SliderSettings EditSlider(int sliderId, SliderSettings userDetails);
    List<SliderSettings> GetAllSliders(bool filterEnabled);

    SliderSettings GetSliderById(int sliderId);
}

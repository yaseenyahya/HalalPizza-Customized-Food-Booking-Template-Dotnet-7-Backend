using System.Collections.Generic;

namespace halalpizzabackend.Models
{
    public interface IAddonsService
    {
        // Create a new deal
        Addons AddAddon(Addons addon);

        // Get all deals
        List<Addons> GetAllAddons();

        // Get a specific deal by ID
        Addons GetAddonById(int id);

        // Update a deal by ID
        Addons EditAddon(int id, Addons updatedAddon);

        // Delete a deal by ID
        void DeleteAddon(int id);
    }
}

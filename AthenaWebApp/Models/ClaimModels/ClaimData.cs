using System;
using System.Collections.Generic;

namespace AthenaWebApp.Models.ClaimModels

{
    public static class ClaimData
    {
        // For all Views
        public static List<String> ClaimTypes = new List<String>() {
            "User","Company","Template","Activities","UserActivity","Roles","Claims"
        };

        // Default Values
        public static List<String> ClaimValues = new List<String>() {
            "Create","Edit","Details","Delete"
        };
    }
}

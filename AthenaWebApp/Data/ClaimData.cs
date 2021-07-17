using System;
using System.Collections.Generic;

namespace AthenaWebApp.Data

{
    public static class ClaimData
    {
        // For all Views
        public static List<String> ClaimTypes = new List<String>() {
            "Create User",          "Edit User",            "Delete User",
            "Create Company",       "Edit Company",         "Delete Company",
            "Create Template",      "Edit Template",        "Delete Template",
            "Create Activities",    "Edit Activites",       "Delete Activites",
            "Create UserActivity",  "Edit UserActivity",    "Delete UserActivity",
            "Create Roles",         "Edit Roles",           "Delete Roles",
            "Create Claims",        "Edit Claims",          "Delete Claims"
        };

        // Default Values
        public static List<String> ClaimValues = new List<String>() {
            "false"
        };
    }
}

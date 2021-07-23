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
            "Create Template",      "Edit Template",        "Delete Template",       "Send Template",
            "Create Activity",      "Edit Activity",        "Delete Activity",
            "Create UserActivity",  "Edit UserActivity",    "Delete UserActivity",
            "Create Role",          "Edit Role",            "Delete Role",
            "Create Badge",         "Edit Badge",           "Delete Badge",
            "Create Claim",         "Edit Claim",           "Delete Claim",
            "Create News",          "Edit News",            "Delete News"

        };

        // Default Values
        public static List<String> ClaimValues = new List<String>() {
            "false"
        };
    }
}

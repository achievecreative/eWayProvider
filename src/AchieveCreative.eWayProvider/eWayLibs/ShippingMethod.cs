using System.ComponentModel;

namespace AchieveCreative.eWayProvider.eWayLibs
{
    public enum ShippingMethod
    {
        /// <summary>Unknown / Not Provided</summary>
        [Description("Unknown / Not Provided")]
        Unknown = 0,

        /// <summary>Low Cost</summary>
        [Description("Low Cost")]
        LowCost,

        /// <summary>Designated By Customer</summary>
        [Description("Designated By Customer")]
        DesignatedByCustomer,

        /// <summary>International</summary>
        [Description("International")]
        International,

        /// <summary>Military</summary>
        [Description("Military")]
        Military,

        /// <summary>Next Day</summary>
        [Description("Next Day")]
        NextDay,

        /// <summary>Store Pickup</summary>
        [Description("Store Pickup")]
        StorePickup,

        /// <summary>Two Day Service</summary>
        [Description("Two Day Service")]
        TwoDayService,

        /// <summary>Three Day Service</summary>
        [Description("Three Day Service")]
        ThreeDayService,

        /// <summary>Other</summary>
        [Description("Other")]
        Other
    }
}
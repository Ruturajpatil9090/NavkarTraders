using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Device.Location;
/// <summary>
/// Summary description for CompLocation
/// </summary>
public class CompLocation
{
    public CompLocation()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    static void ResolveAddressSync()
    {
        GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
        watcher.MovementThreshold = 1.0;
        watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));
        CivicAddressResolver resolver = new CivicAddressResolver();
        if (watcher.Position.Location.IsUnknown == false)
        {
            CivicAddress address = resolver.ResolveAddress(watcher.Position.Location);
            if (!address.IsUnknown)
            {
                string country = address.CountryRegion;
                string postalcode = address.PostalCode;
                string state = address.StateProvince;
            }
        }
    }
}
using System.Threading.Tasks;
using GhostHuntAR.Infrastructure.Models;
using Supermortal.Common.PCL.Abstract;

namespace GhostHuntAR.Infrastructure.Abstract
{
    public interface IGHGeocoderService : IGeocoderService
    {
        Task ProcessGHLocation(GHLocation location);
    }
}

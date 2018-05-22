namespace DdSG {

    public static class AssetExtensions {

        public static Asset WithAssetSocketIndex(this Asset asset, int index) {
            if (asset != null) {
                asset.AssetSocketIndex = index;
            }
            return asset;
        }

    }

}

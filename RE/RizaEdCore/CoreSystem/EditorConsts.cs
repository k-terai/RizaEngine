// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace RizaEdCore.CoreSystem
{
    public static class EditorConsts
    {
        // Projects.
        public const string PROJECT_DATA_EXTENSION = ".project";
        public const string ASSETS_DIRECTORY_NAME = "Assets";
        public const string RUNTIME_DIRECTORY_NAME = "Runtime";
        public const string PLUGIN_DIRECTORY_NAME = "Plugins";
        public const string PROJECTSETTINGS_DIRECTORY_NAME = "ProjectSettings";
        public const string USERSETTINGS_DIRECTORY_NAME = "UserSettings";
        public const int MAX_PROJECTNAME_LENGTH = 20;

        //Assets.
        public const string ASSET_METADATA_EXTENSION = ".meta";
        public const int MAX_ASSET_NAME_LENGTH = 50;
        public const string ASSET_IMPORT_FILTER = "Asset files(*.png *.jpg *.obj *.fbx)|*.png;*.fbx;*.obj;*.jpg";


        // Engines.
        public const string ENGINE_VERSION = "1.0.0";

        // Thumbnail.
        public const string THUMBNAIL_FILE_IMAGE_EXTENSION = ".png";
        public const int THUMBNAIL_FILE_SIZE = 256;
        public const int TINY_THUMBNAIL_SIZE = 40;
        public const int SMALL_THUMBNAIL_SIZE = 80;
        public const int MEDIUM_THUMBNAIL_SIZE = 150;
        public const int LARGE_THUMBNAIL_SIZE = 200;
        public const int HUGE_THUMBNAIL_SIZE = 300;

        // Language code
        public const string JAPANACECODE = "ja-jp";
        public const string ENGLISHCODE = "en-us";
    }
}


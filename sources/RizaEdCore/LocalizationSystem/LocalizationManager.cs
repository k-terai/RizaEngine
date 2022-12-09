// Copyright (c) k-terai and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using static RizaEdCore.CoreSystem.EditorCommon;
using static RizaEdCore.CoreSystem.EditorConsts;

namespace RizaEdCore.LocalizationSystem
{
    public static class LocalizationManager
    {
        /// <summary>
        /// Using check project name error.
        /// </summary>
        private static Encoding s_Shift_JIS_Encoding = null;

        private static Dictionary<string, Dictionary<Result, string>> s_editorResultStrings = null;

        public static Encoding Shift_JIS_Encoding
        {
            get
            {
                if (s_Shift_JIS_Encoding == null)
                {
                    s_Shift_JIS_Encoding = Encoding.GetEncoding("Shift_JIS");
                }

                return s_Shift_JIS_Encoding;
            }
        }

        public static void Initialize()
        {
            s_editorResultStrings = new Dictionary<string, Dictionary<Result, string>>();

            var en_us = CultureInfo.GetCultureInfo("en-US");
            var ja_jp = CultureInfo.GetCultureInfo("ja-JP");

            //TODO: Remove external files(Ex.excel).
            s_editorResultStrings[en_us.Name] = new Dictionary<Result, string>();
            s_editorResultStrings[ja_jp.Name] = new Dictionary<Result, string>();

            s_editorResultStrings[en_us.Name][Result.ERROR_PROJECTNAME_MIN] = "Project name error. A character has not been entered.";
            s_editorResultStrings[ja_jp.Name][Result.ERROR_PROJECTNAME_MIN] = "プロジェクト名エラー。入力されていません。";

            s_editorResultStrings[en_us.Name][Result.ERROR_PROJECTNAME_MAX] = $"Project name error.name is must be {MAX_PROJECTNAME_LENGTH} or less.";
            s_editorResultStrings[ja_jp.Name][Result.ERROR_PROJECTNAME_MAX] = $"プロジェクト名エラー。プロジェクト名は {MAX_PROJECTNAME_LENGTH} 以下である必要があります。";

            s_editorResultStrings[en_us.Name][Result.ERROR_PROJECTNAME_INVALID] = "Project name error. Invalid character in project name.";
            s_editorResultStrings[ja_jp.Name][Result.ERROR_PROJECTNAME_INVALID] = "プロジェクト名エラー。プロジェクト名に無効な文字が含まれています。";

            s_editorResultStrings[en_us.Name][Result.ERROR_PROJECTNAME_DOUBLEBYTE] = "Project name error. Contains double-byte characters.";
            s_editorResultStrings[ja_jp.Name][Result.ERROR_PROJECTNAME_DOUBLEBYTE] = "プロジェクト名エラー。マルチバイト文字が含まれています。";

            s_editorResultStrings[en_us.Name][Result.ERROR_PROJECTNAME_PATH_EXISTS] = "Path error. A folder with that name already exists at that location.";
            s_editorResultStrings[ja_jp.Name][Result.ERROR_PROJECTNAME_PATH_EXISTS] = "プロジェクトパスエラー。指定されたパスには既に別のフォルダが存在しています。";

            s_editorResultStrings[en_us.Name][Result.ERROR_PROJECTPATH_NOT_EXISTS] = "Location error. A folder with that name not exists.";
            s_editorResultStrings[ja_jp.Name][Result.ERROR_PROJECTPATH_NOT_EXISTS] = "プロジェクトパスエラー。指定されたパスにはフォルダが見つかりません。";

            s_editorResultStrings[en_us.Name][Result.ERROR_ASSETNAME_MIN] = "Asset name error.A character has not been entered.";
            s_editorResultStrings[ja_jp.Name][Result.ERROR_ASSETNAME_MIN] = " アセット名エラー。入力されていません。";

            s_editorResultStrings[en_us.Name][Result.ERROR_ASSETNAME_MAX] = $"Asset name error.name is must be {MAX_ASSET_NAME_LENGTH} or less.";
            s_editorResultStrings[ja_jp.Name][Result.ERROR_ASSETNAME_MAX] = $"アセット名エラー。アセット名は {MAX_ASSET_NAME_LENGTH} 以下である必要があります。";

            s_editorResultStrings[en_us.Name][Result.ERROR_ASSETNAME_INVALID] = $"Asset name error.Invalid character in asset name.";
            s_editorResultStrings[ja_jp.Name][Result.ERROR_ASSETNAME_INVALID] = $"アセット名エラー。プロジェクト名に無効な文字が含まれています。";

            s_editorResultStrings[en_us.Name][Result.ERROR_ASSETNAME_DOUBLEBYTE] = "Asset name error. Contains double-byte characters.";
            s_editorResultStrings[ja_jp.Name][Result.ERROR_ASSETNAME_DOUBLEBYTE] = "アセット名エラー。マルチバイト文字が含まれています。";

            s_editorResultStrings[en_us.Name][Result.ERROR_ASSETNAME_SAMENAME] = "Asset name error. Same name asset already exists.";
            s_editorResultStrings[ja_jp.Name][Result.ERROR_ASSETNAME_SAMENAME] = "アセット名エラー。同名アセットが既に存在しています。";
        }

        public static string GetString(Result result)
        {
            var name = CultureInfo.CurrentCulture.Name;

            if (s_editorResultStrings == null || !s_editorResultStrings.ContainsKey(name) || !s_editorResultStrings[name].ContainsKey(result))
            {
                return string.Empty;
            }

            return s_editorResultStrings[name][result];
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashW.Utils
{
    public sealed class AppContract
    {
        public const string BIN_DIR = @"./bin/";
        public const string DOWNLOAD_TEMP_DIR = @"./temp/";
        public const string LOG_DIR = @"./logs/";
        public const string CLASH_DIR = BIN_DIR;
        public const string CLASH_EXE_NAME = "clash-win64.exe";
        public const string CLASH_PROCESS_NAME = "clash-win64";
        public const string CLASH_CONFIG_NAME = "config.yml";
        public const string CLASH_GEOIP_NAME = "Country.mmdb";
        public const string CLASH_DOWNLOAD_NAME = "clash-win64.zip";
        public const string GEOIP_DOWNLOAD_NAME = "GeoLite2-Country.tar.gz";
        public static readonly string CLASH_EXE_PATH = $"{BIN_DIR}{CLASH_EXE_NAME}";
        public static readonly string CLASH_CONFIG_PATH = $"{BIN_DIR}{CLASH_CONFIG_NAME}";
        public static readonly string CLASH_GEOIP_PATH = $"{BIN_DIR}{CLASH_GEOIP_NAME}";
        public static readonly string CLASH_DOWNLOAD_PATH = $"{DOWNLOAD_TEMP_DIR}{CLASH_DOWNLOAD_NAME}";
        public static readonly string GEOIP_DOWNLOAD_PATH = $"{DOWNLOAD_TEMP_DIR}{GEOIP_DOWNLOAD_NAME}";

        public const string CLASHW_LOG_NAME = "output.log";
        public static readonly string CLASHW_LOG_PATH = $"{LOG_DIR}output.log";
    }
}

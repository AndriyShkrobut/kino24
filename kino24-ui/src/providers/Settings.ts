import { ColorMode, Layout, LayoutSize } from 'models/enums';

const SETTINGS_LOCAL_STORAGE_KEY = 'settings';

class Settings {
    private static instance: Settings;

    private constructor(
        public colorMode: ColorMode = ColorMode.LIGHT,
        public layout: Layout = Layout.FIXED,
        public layoutSize: LayoutSize = LayoutSize.LG
    ) {
        const savedSettingsJson = window.localStorage.getItem(SETTINGS_LOCAL_STORAGE_KEY);

        if (savedSettingsJson) {
            this._setSavedSettings(savedSettingsJson);
        } else {
            this.saveSettings();
        }
    }

    private _setSavedSettings = (savedSettingsJson: string) => {
        const savedSettings: Settings = JSON.parse(savedSettingsJson);

        if (savedSettings) {
            this.colorMode = savedSettings.colorMode;
            this.layout = savedSettings.layout;
            this.layoutSize = savedSettings.layoutSize;
        }
    };

    public static getInstance = () => {
        if (!Settings.instance) {
            Settings.instance = new Settings();
        }

        return Settings.instance;
    };

    public saveSettings = () => {
        const settingsJson = JSON.stringify(this);
        window.localStorage.setItem(SETTINGS_LOCAL_STORAGE_KEY, settingsJson);
    };

    public toggleColorMode = () => {
        if (this.colorMode === ColorMode.LIGHT) {
            this.colorMode = ColorMode.DARK;
        } else {
            this.colorMode = ColorMode.LIGHT;
        }

        this.saveSettings();
    };

    public setLayout = (layout: Layout) => {
        this.layout = layout;
        this.saveSettings();
    };

    public setLayoutSize = (layoutSize: LayoutSize) => {
        this.layoutSize = layoutSize;
        this.saveSettings();
    };

    public toString = () => {
        return JSON.stringify(this);
    };
}

export default Settings.getInstance();

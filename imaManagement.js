// imaManagement.js
// קובץ ניהול מרכזי עבור Ima / The Eye
// כולל כל הרמות וההיבטים של פרויקט Ima

export const ImaManagement = {

  /** ===== פרופיל עולמי ומותג ===== */
  brand: {
    name: "Ima",
    logo: "server/public/logo.png",
    tagline: "The Eye of Inner Growth",
    colors: {
      primary: "#FFB6A0",
      secondary: "#224466"
    },
    globalIdentity: true
  },

  /** ===== שלבי הפצה ===== */
  distribution: {
    pilot: {
      description: "שחרור למשתמשים בודדים וקהילות קטנות",
      active: true
    },
    corporate: {
      description: "שיתופי פעולה עם חברות חינוך, בריאות ורווחה",
      active: false
    },
    regional: {
      description: "הפצה לפי מדינות ואזורים",
      active: false
    },
    global: {
      description: "השקה עולמית",
      active: false
    }
  },

  /** ===== שיווק ומכירות ===== */
  marketing: {
    campaigns: [],
    socialMedia: ["Facebook","Instagram","TikTok","YouTube"],
    stores: ["App Store","Google Play"],
    referralSystem: true,
    analytics: true
  },

  sales: {
    premiumFeatures: true,
    inGamePurchases: true,
    userLevels: true,
    revenueTracking: true
  },

  /** ===== פיתוח ומוצר ===== */
  product: {
    modules: ["Ima3D","EyeSpace","AIInteraction","VoiceChat","ProceduralModels"],
    version: "0.1.0",
    updates: [],
    releaseNotes: [],
    devTeam: ["frontEnd","backEnd","3DArtists","AI"]
  },

  /** ===== פרופיל משתמש ודינמיקה ===== */
  users: {
    profileTemplate: {
      age: 10,
      progress: 0,
      regulation: 0,
      voiceEnabled: true,
      language: "he",
      lastSession: null
    },
    adaptiveSettings: {
      eyeSpaceScale: 1,
      emotionSensitivity: 0.05,
      modelVariation: true
    }
  },

  /** ===== AI ולמידה מתמדת ===== */
  AI: {
    enabled: true,
    learningMode: true,
    emotionDetection: true,
    suggestions: true,
    logging: true
  },

  /** ===== ניהול ופיקוח ===== */
  operations: {
    execDashboard: true,
    distributionReports: true,
    userAnalytics: true,
    KPItracking: true
  },

  /** ===== פונקציות שימושיות ===== */
  setDistributionStage(stage){
    Object.keys(this.distribution).forEach(s => this.distribution[s].active = false);
    if(this.distribution[stage]) this.distribution[stage].active = true;
  },

  addCampaign(campaign){
    this.marketing.campaigns.push(campaign);
  },

  logUpdate(module, note){
    const timestamp = new Date().toISOString();
    this.product.updates.push({module,note,timestamp});
  },

  addReleaseNote(version, note){
    const timestamp = new Date().toISOString();
    this.product.releaseNotes.push({version,note,timestamp});
  },

  getActiveDistribution(){
    return Object.entries(this.distribution).find(([k,v]) => v.active)?.[0] || null;
  }

};

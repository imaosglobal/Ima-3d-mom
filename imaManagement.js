// imaManagement.js
// קובץ ניהול מרכזי עבור Ima / The Eye
// כולל כל הרמות, למידה מתמדת, ושכפול למודולים חדשים

export const ImaManagement = {

  /** ===== זהות ומותג ===== */
  brand: {
    name: "Ima",
    logo: "server/public/logo.png",
    tagline: "The Eye of Inner Growth",
    colors: { primary:"#FFB6A0", secondary:"#224466" },
    globalIdentity:true
  },

  /** ===== שלבי הפצה ===== */
  distribution: {
    pilot:{ description:"משתמשים בודדים/קהילות קטנות", active:true },
    corporate:{ description:"חברות חינוך, בריאות, רווחה", active:false },
    regional:{ description:"מדינות ואזורים", active:false },
    global:{ description:"השקה עולמית", active:false }
  },

  /** ===== שיווק ומכירות ===== */
  marketing: {
    campaigns:[],
    socialMedia:["Facebook","Instagram","TikTok","YouTube"],
    stores:["App Store","Google Play"],
    referralSystem:true,
    analytics:true
  },

  sales: {
    premiumFeatures:true,
    inGamePurchases:true,
    userLevels:true,
    revenueTracking:true
  },

  /** ===== פיתוח ומוצר ===== */
  product: {
    modules:["Ima3D","EyeSpace","AIInteraction","VoiceChat","ProceduralModels"],
    version:"0.1.0",
    updates:[],
    releaseNotes:[],
    devTeam:["frontEnd","backEnd","3DArtists","AI"]
  },

  /** ===== פרופיל משתמש ודינמיקה ===== */
  users: {
    profileTemplate: { age:10, progress:0, regulation:0, voiceEnabled:true, language:"he", lastSession:null },
    adaptiveSettings:{ eyeSpaceScale:1, emotionSensitivity:0.05, modelVariation:true }
  },

  /** ===== AI ולמידה מתמדת ===== */
  AI: {
    enabled:true,
    learningMode:true,
    emotionDetection:true,
    suggestions:true,
    logging:true,
    /** מנגנון Modular Self-Learning */
    modulesLearning:{}, // כל מודול/תחום שייקרא כאן ילמד עצמאי
    learnModule(moduleName, data){
      if(!this.modulesLearning[moduleName]) this.modulesLearning[moduleName] = [];
      this.modulesLearning[moduleName].push(data);
    }
  },

  /** ===== ניהול ופיקוח ===== */
  operations: {
    execDashboard:true,
    distributionReports:true,
    userAnalytics:true,
    KPItracking:true
  },

  /** ===== פונקציות שימושיות ===== */
  setDistributionStage(stage){
    Object.keys(this.distribution).forEach(s=>this.distribution[s].active=false);
    if(this.distribution[stage]) this.distribution[stage].active=true;
  },

  addCampaign(campaign){
    this.marketing.campaigns.push(campaign);
  },

  logUpdate(module,note){
    const timestamp = new Date().toISOString();
    this.product.updates.push({module,note,timestamp});
  },

  addReleaseNote(version,note){
    const timestamp = new Date().toISOString();
    this.product.releaseNotes.push({version,note,timestamp});
  },

  getActiveDistribution(){
    return Object.entries(this.distribution).find(([k,v])=>v.active)?.[0] || null;
  },

  /** ===== יצירת מודול חדש וניהול עצמאי ===== */
  createNewModule(moduleName){
    if(this[moduleName]) return console.warn("Module exists:",moduleName);
    this[moduleName] = {
      users:[],
      profileTemplate:{...this.users.profileTemplate},
      adaptiveSettings:{...this.users.adaptiveSettings},
      AI:{ learningData:[], learn: function(data){ this.learningData.push(data); } },
      log:[],
      addUser(u){ this.users.push(u); },
      addLog(note){ this.log.push({note, timestamp:new Date().toISOString()}); }
    };
    console.log("Module created:",moduleName);
  },

  /** ===== דוחות למנכ"ל ומפיצים ===== */
  generateReport(){
    return {
      distribution:this.getActiveDistribution(),
      campaigns:this.marketing.campaigns,
      product:this.product,
      usersCount:this.users.profileTemplate ? 1 : 0,
      AIlearn:this.AI.modulesLearning
    };
  }
};

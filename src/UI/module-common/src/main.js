import WebHost from 'netmodular-module-admin'
import Common from './index'
import config from './config'

// 注册模块
WebHost.registerModule(Common)

// 启动
WebHost.start(config)

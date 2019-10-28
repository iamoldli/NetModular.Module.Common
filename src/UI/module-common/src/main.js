import WebHost from 'netmodular-module-admin'
import config from './config'
import Common from './index'

// 注册模块
WebHost.registerModule(Common)

// 启动
WebHost.start(config)

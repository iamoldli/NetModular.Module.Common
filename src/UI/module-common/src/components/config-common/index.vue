<template>
  <nm-form-page v-bind="form">
    <el-row :gutter="20">
      <el-col :span="10" :offset="1">
        <el-form-item label="字典缓存：" prop="dictCacheEnabled">
          <el-switch v-model="form.model.dictCacheEnabled" />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-page>
</template>
<script>
import { mixins } from 'netmodular-module-admin'
import module from '../../module'
export default {
  mixins: [mixins.configForm],
  data() {
    return {
      code: module.code,
      form: {
        labelWidth: '200px',
        model: {
          dictCacheEnabled: false
        }
      }
    }
  }
}
</script>

<template>
  <nm-form-page v-bind="form">
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="字典缓存：" prop="dictCacheEnabled">
          <el-switch v-model="form.model.dictCacheEnabled" />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-page>
</template>
<script>
import module from '../../module'
const { edit, update } = $api.admin.config
export default {
  data() {
    return {
      code: module.code,
      type: 1,
      form: {
        header: false,
        action: this.update,
        labelWidth: '200px',
        model: {
          dictCacheEnabled: false
        }
      }
    }
  },
  methods: {
    update() {
      return update({ type: this.type, code: this.code, json: JSON.stringify(this.form.model) })
    }
  },
  created() {
    edit({ type: this.type, code: this.code }).then(data => {
      this.form.model = data
    })
  }
}
</script>

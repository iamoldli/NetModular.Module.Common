<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" clearable autofocus />
        </el-form-item>
        <el-form-item label="值：" prop="value">
          <el-input v-model="form.model.value" clearable />
        </el-form-item>
        <el-form-item label="扩展数据：" prop="extend">
          <el-input type="textarea" v-model="form.model.extend" clearable />
        </el-form-item>
        <el-form-item label="图标：" prop="icon">
          <el-input v-model="form.model.icon" clearable />
        </el-form-item>
        <el-form-item label="排序：" prop="sort">
          <el-input type="number" v-model.number="form.model.sort" clearable />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'netmodular-ui'

// 接口
const api = $api.common.dictItem

export default {
  mixins: [mixins.formDialogEdit],
  data() {
    return {
      api,
      form: {
        title: '编辑字典',
        action: api.update,
        width: '650px',
        model: {
          /** 名称 */
          name: '',
          /** 值 */
          value: '',
          extend: '',
          icon: '',
          /** 排序 */
          sort: ''
        },
        rules: {
          name: [{ required: true, message: '请输入名称' }],
          code: [{ required: true, message: '请输入编码' }],
          sort: [
            { required: true, message: '请输入序号' },
            { type: 'number', message: '序号必须为数字' }
          ]
        }
      }
    }
  }
}
</script>

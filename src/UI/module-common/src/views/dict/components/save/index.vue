<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="分组：" prop="groupCode">
          <group-select v-model="form.model.groupCode" clearable />
        </el-form-item>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" clearable autofocus />
        </el-form-item>
        <el-form-item label="编码：" prop="code">
          <el-input v-model="form.model.code" clearable />
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
import GroupSelect from '../../../dictGroup/components/select'

// 接口
const { add, edit, update } = $api.common.dict

export default {
  mixins: [mixins.formSave],
  components: { GroupSelect },
  data() {
    return {
      title: '字典',
      actions: {
        add,
        edit,
        update
      },
      form: {
        width: '650px',
        model: {
          groupCode: '',
          /** 名称 */
          name: '',
          /** 值 */
          code: '',
          /** 排序 */
          sort: ''
        },
        rules: {
          groupCode: [{ required: true, message: '请选择分组' }],
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

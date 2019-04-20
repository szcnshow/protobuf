namespace FlatBuffers
{
    public static class FlatBufferHelper
    {
        /// <summary>
        /// Build vector from c# array
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="builder">flatBufferBuilder instance</param>
        /// <param name="datas">c# array</param>
        /// <returns></returns>
        public static VectorOffset BuildVector<T>(this FlatBufferBuilder builder, T[] datas) where T : struct
        {
            int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
            builder.StartVector(size, datas.Length, size);
            builder.Add(datas);
            return builder.EndVector();
        }

        /// <summary>
        /// Build string
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static StringOffset BuildString(this FlatBufferBuilder builder, string data)
        {
            return data == null ? default(StringOffset) : builder.CreateString(data);
        }

        /// <summary>
        /// Decode vector to c# array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="fieldName"> C# class property's name</param>
        /// <returns></returns>
        public static T[] DecodeVector<T>(this FlatPLsModelData data, string fieldName)
        {
            //get the vector length
            var prop = typeof(FlatPLsModelData).GetProperty(fieldName + "Length");
            int len = (int)prop.GetValue(data);
            if (len == 0)
                return null;

            //get the vector access method
            var method = typeof(FlatPLsModelData).GetMethod(fieldName);

            T[] retData = new T[len];

            for (int i = 0; i < len; i++)
            {
                object[] paras = new object[] { i };
                retData[i] = (T)method.Invoke(data, paras);
            }

            return retData;
        }
    }
}
